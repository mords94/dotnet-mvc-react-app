


using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dotnet.ViewModel.Paging;

public interface IListMapper
{
    IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> fromList);
    Page<TTo> MapPage<TFrom, TTo>(Page<TFrom> fromList);
}