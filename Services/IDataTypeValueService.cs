using Microsoft.AspNetCore.Mvc.Rendering;

namespace DenKhangThinh.Services;

public interface IDataTypeValueService
{
    IEnumerable<SelectListItem> GetItemsFromValueListDataType(string dataTypeName, string[] selectedValues);
}
