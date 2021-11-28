


using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dotnet.ViewModel.Paging;

public class ListMapper : IListMapper
{


    private IMapper Mapper;

    public ListMapper(IMapper mapper)
    {
        Mapper = mapper;
    }

    public IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> fromList)
    {
        return fromList.Select(item => Mapper.Map<TFrom, TTo>(item));
    }

    public Page<TTo> MapPage<TFrom, TTo>(Page<TFrom> page)
    {
        return new Page<TTo>(Map<TFrom, TTo>(page.Content), page.Pageable, page.TotalElements);
    }
}
