using System.Collections.Generic;
using MvvmHelpers;
using eForm.Models.NavigationMenu;

namespace eForm.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}