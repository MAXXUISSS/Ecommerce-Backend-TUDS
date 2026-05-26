using ECommerce.Application.CQRS;
using ECommerce.Application.Common;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.UseCases.Products.Queries;

public record GetPagedProductsQuery(int Page, int PageSize) : IQuery<PagedData<Product>>;
