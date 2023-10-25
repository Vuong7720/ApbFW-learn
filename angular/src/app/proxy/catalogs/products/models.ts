import type { ProductType } from '../../tedu-ecommance/products/product-type.enum';
import type { BaseListFilterDto } from '../../models';
import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateProductsDto {
  manufacturerId?: string;
  name?: string;
  code?: string;
  slug?: string;
  productType: ProductType;
  sku?: string;
  sortOder: number;
  visibility: boolean;
  isActive: boolean;
  sellPrice: number;
  categoryId?: string;
  seoMetaDescription?: string;
  description?: string;
  thumnailPictureName?: string;
  thumnailPictureContent?: string;
}

export interface ProductListFilterDto extends BaseListFilterDto {
  categoryId?: string;
}

export interface ProductsDto {
  manufacturerId?: string;
  name?: string;
  code?: string;
  slug?: string;
  productType: ProductType;
  sku?: string;
  sortOder: number;
  visibility: boolean;
  isActive: boolean;
  sellPrice: number;
  categoryId?: string;
  seoMetaDescription?: string;
  description?: string;
  thumnailPicture?: string;
  id?: string;
  categoryName?: string;
  categorySlug?: string;
}

export interface ProductsInListDto extends EntityDto<string> {
  manufacturerId?: string;
  name?: string;
  code?: string;
  productType: ProductType;
  sku?: string;
  sortOder: number;
  visibility: boolean;
  isActive: boolean;
  categoryId?: string;
  thumnailPicture?: string;
  categoryName?: string;
  categorySlug?: string;
}
