using ECommerce.Application.Common;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.UseCases.Products.Queries;

public record GetPagedProductsQuery(int Page, int PageSize) : IRequest<PagedData<Product>>;
