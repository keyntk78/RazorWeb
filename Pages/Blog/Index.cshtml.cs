using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorWeb.Models;

namespace RazorWeb.Pages.Shared.Blog
{
    public class IndexModel : PageModel
    {
        private readonly RazorWeb.Models.MyBlogContext _context;
        public const int ITEMS_PER_PAGE = 10;
        [BindProperty(SupportsGet =true, Name ="p")]
        public int currentPage { get; set; }
        public int countPages { get; set; }

        public IndexModel(RazorWeb.Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync(string searchString)
        {

            int totalArticle = await _context.Articles.CountAsync();

            countPages = (int)Math.Ceiling((double)totalArticle/ITEMS_PER_PAGE);

            if(currentPage<1)
                currentPage= 1;
            if (currentPage > countPages)
                currentPage = countPages;

            if (_context.Articles != null)
            {
                //Article = await _context.Articles.ToListAsync();
                var qr = (from a in _context.Articles
                         orderby a.Creadted descending
                         select a).Skip((currentPage-1)*ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE);

                if (!string.IsNullOrEmpty(searchString))
                {
                   Article = qr.Where(a => a.Title.Contains(searchString)).ToList();
                } else
                {
                    Article = await qr.ToListAsync();
                } 

                
            }
        }
    }
}
