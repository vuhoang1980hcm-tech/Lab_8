using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.CoursePages;

public class CreateModel : PageModel
{
    private readonly SchoolContext _context;

    public CreateModel(SchoolContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        LoadDepartments();
        return Page();
    }

    [BindProperty]
    public Course Course { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadDepartments();
            return Page();
        }

        _context.Courses.Add(Course);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    // Shows "ID - Name" in the Department dropdown instead of just the ID.
    private void LoadDepartments()
    {
        if (_context.Departments == null || !_context.Departments.Any())
        {
            ViewData["DepartmentID"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return;
        }

        ViewData["DepartmentID"] = new SelectList(
            _context.Departments.Select(d => new
            {
                d.DepartmentID,
                DisplayText = d.DepartmentID + " - " + d.DepartmentName
            }),
            "DepartmentID",
            "DisplayText"
        );
    }
}