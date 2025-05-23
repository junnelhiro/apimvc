using ConsoleApp1.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace ConsoleApp1;
public class ClientAuth
{
    public ClientAuth()
    {
        int[] roles = new int[0];
        IAuthorization authorization = AuthFactory.Create();
        Console.WriteLine(authorization.IsAuthorize("F1", "Edit"));
        Console.WriteLine(authorization.IsAuthorize("F2", "Edit"));
        Console.WriteLine(authorization.IsAuthorize("F2", "Editx"));
    }
}
public class CommandRegistration : IAuthCommandManagement
{
    private readonly IAuthorization _authorization;
    public CommandRegistration(IAuthorization authorization)
    {
    }
    public void Register(string module_name, string command_group, string[] labels)
    {
        string[] commandGroups = [];
        Reset(module_name);
        _authorization.Invalidate(commandGroups);
        //store to db
    }
    public void Reset(string module_name)
    {
        //remove from label Registration
        //remove from RoleLabel
    }
}
public class Authorization : IAuthorization
{
    private Dictionary<string, string[]> commands;
    public Authorization()
    {
        commands = new Dictionary<string, string[]>();
    }
    public void SetCommands(Dictionary<string, string[]> commands)
    {
        commands.Clear();
        commands = commands ?? new Dictionary<string, string[]>();
    }
    public bool IsAuthorize(string command_group, string command_name)
    {
        return commands.TryGetValue(command_group, out var _commands)
            && (_commands ?? Array.Empty<string>()).Contains(command_name);
    }
    public void Invalidate(string[] command_groups)
    {
        foreach (var item in command_groups)
        {
            commands.Remove(item);
        }
    }
}
public class RoleUserManager
{
    private List<RoleUserModel> _userRoles = new List<RoleUserModel>();
    private static int Id;
    public RoleUserManager Create(int userId, int[] roles)
    {
        for (int i = 0; i < roles.Length - 1; i++)
        {
            _userRoles.Add(new RoleUserModel { Id = Id++, UserId = userId, RoleId = i });
        }
        return this;
    }
    public List<RoleUserModel> GetRoles()
    {
        return _userRoles;
    }
}
public class RoleManagemer
{

    private List<RoleModel> _roles = new List<RoleModel>();
    private static int Id;
    public RoleManagemer Create(string roleName)
    {
        _roles.Add(new RoleModel { Id = Id++, Name = roleName });
        return this;
    }
    public List<RoleModel> GetRoles()
    {
        return _roles;
    }
}
public class RoleCommandManager
{
    private readonly AppDbContext context;
    public RoleCommandManager(AppDbContext context)
    {
        this.context = context;
    }
    public void RemoveRoleCommand(int roleId, int[] actionIds)
    {
        var actions = context.RoleAuthLabels
            .Where(x => x.RoleId == roleId
            && actionIds.Any(xx => xx == x.ActionId));
        if (actions == null && !actions.Any())
            return;
        context.RoleAuthLabels.RemoveRange(actions);
    }
    public void AddRoleCommand(int roleId, int[]  actionIds)
    {
        RemoveRoleCommand(roleId, actionIds);//remove existing
        foreach (var actionId in actionIds)
        {
            context.RoleAuthLabels.Add(new RoleAuthLabelModel
            {
                ActionId = actionId,
                RoleId = roleId,
            });
        }
    }
    public Dictionary<string, string[]> GetAll(int[] roles)
    {
        return context.RoleAuthLabels
            .Where(x => roles.Any(xx => xx == x.RoleId))
            .GroupBy(x => x.Action.group_name)
            .Select(x => new { x.Key, val = x.Select(x => x.Action.command_name).ToArray() })
            .ToDictionary(x => x.Key, x => x.val) ?? new Dictionary<string, string[]>();
        ;
    }
}
public class AuthFactory
{
    public static IAuthorization Create()
    {
        return new Authorization();
    }
}
public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
}
public class RoleModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class AuthLabelModel
{
    public int Id { get; set; }
    public string module { get; set; }
    public string group_name { get; set; }
    public string command_name { get; set; }
}
public class RoleAuthLabelModel
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int ActionId { get; set; }
    public AuthLabelModel Action { get; set; }
}
public class RoleUserModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
}
public interface IAuthCommandManagement
{
    void Reset(string module_name);
    void Register(string module_name, string command_group, string[] labels);
}
public interface IAuthorization
{
    bool IsAuthorize(string command_group, string command_name);
    void Invalidate(string[] command_group);
}