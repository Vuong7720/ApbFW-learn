import type { AddUpdateProductAttributeDto, ProductAttributeListFilterDto, ProductAttributeValueDto } from './attributes/models';
import type { CreateUpdateProductsDto, ProductListFilterDto, ProductsDto, ProductsInListDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  apiName = 'Default';
  

  addAttribute = (input: AddUpdateProductAttributeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductAttributeValueDto>({
      method: 'POST',
      url: '/api/app/products/attribute',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateUpdateProductsDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductsDto>({
      method: 'POST',
      url: '/api/app/products',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/products/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMutiple = (ids: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/products/mutiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductsDto>({
      method: 'GET',
      url: `/api/app/products/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: PagedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProductsDto>>({
      method: 'GET',
      url: '/api/app/products',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListAll = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductsInListDto[]>({
      method: 'GET',
      url: '/api/app/products/all',
    },
    { apiName: this.apiName,...config });
  

  getListFilter = (input: ProductListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProductsInListDto>>({
      method: 'GET',
      url: '/api/app/products/filter',
      params: { categoryId: input.categoryId, keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getListProductAttributeAll = (productId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductAttributeValueDto[]>({
      method: 'GET',
      url: `/api/app/products/product-attribute-all/${productId}`,
    },
    { apiName: this.apiName,...config });
  

  getProductAttribute = (input: ProductAttributeListFilterDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProductAttributeValueDto>>({
      method: 'GET',
      url: '/api/app/products/product-attribute',
      params: { productId: input.productId, keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getThumnailImage = (fileName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/products/thumnail-image',
      params: { fileName },
    },
    { apiName: this.apiName,...config });
  

  getsuggestNumber = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'GET',
      responseType: 'text',
      url: '/api/app/products/suggest-number',
    },
    { apiName: this.apiName,...config });
  

  removeAttribute = (attributeId: string, id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/products/${id}/attribute/${attributeId}`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateProductsDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductsDto>({
      method: 'PUT',
      url: `/api/app/products/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateAttribute = (id: string, input: AddUpdateProductAttributeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductAttributeValueDto>({
      method: 'PUT',
      url: `/api/app/products/${id}/attribute`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
