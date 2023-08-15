using Admin_UserAPI.Data;
using Admin_UserAPI.Models;


using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]

[ApiController]

public class AdminUserController : ControllerBase

{

    public readonly DbContextClass adminDbContextClass;

    public AdminUserController(DbContextClass _db)

    {

        adminDbContextClass = _db;

    }

    [HttpGet]

    public async Task<ActionResult> GetAdminUsers()

    {

        return Ok(await adminDbContextClass.AdminUserData.ToListAsync());
    }
    [HttpPost]
    public ActionResult<AdminUser> Postadm([FromBody] AdminUser adm)
    {

        adminDbContextClass.AdminUserData.Add(adm);

        adminDbContextClass.SaveChanges();

        return Ok(adm);
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<AdminUser>> put([FromBody] AdminUser Admin, int id)
    {
        if (id == 0)

        {

            return BadRequest();

        }

        adminDbContextClass.AdminUserData.Update(Admin);

        await adminDbContextClass.SaveChangesAsync();

        return Ok(Admin);

    }

    [HttpDelete("{id:int}")]

    public async Task<ActionResult> Delete(int id)

    {

        var result = adminDbContextClass.AdminUserData.Find(id);

        adminDbContextClass.AdminUserData.Remove(result);

        await adminDbContextClass.SaveChangesAsync();

        return Ok(result);

    }

}