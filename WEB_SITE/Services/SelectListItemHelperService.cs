using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_SITE.Services
{
    public static class SelectListItemHelperService
    {
        public static List<SelectListItem> ToSelectListItems<T>(
       this List<T> items,
       Func<T, string> textSelector,
       Func<T, string> valueSelector,
       string? selectedValue = "")
        {
            return items.ConvertAll(item =>
            {
                var value = valueSelector(item);
                return new SelectListItem
                {
                    Text = textSelector(item),
                    Value = value,
                    Selected = !string.IsNullOrEmpty(selectedValue) && value == selectedValue
                };
            });
        }
    }
}