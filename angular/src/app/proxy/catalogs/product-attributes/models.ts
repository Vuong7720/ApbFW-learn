import type { AttributeType } from '../../tedu-ecommance/product-attributes/attribute-type.enum';
import type { EntityDto } from '@abp/ng.core';

export interface CreateUpdateProductAttributeDto {
  code?: string;
  attributeType: AttributeType;
  lable?: string;
  sortOder?: string;
  visibility: boolean;
  isActive: boolean;
  isRequired: boolean;
  isUnique: boolean;
  note?: string;
}

export interface ProductAttributeDto {
  code?: string;
  attributeType: AttributeType;
  lable?: string;
  sortOder?: string;
  visibility: boolean;
  isActive: boolean;
  isRequired: boolean;
  isUnique: boolean;
  note?: string;
  id?: string;
}

export interface ProductAttributeInListDto extends EntityDto<string> {
  code?: string;
  attributeType: AttributeType;
  lable?: string;
  sortOder?: string;
  visibility: boolean;
  isActive: boolean;
  isRequired: boolean;
  isUnique: boolean;
  id?: string;
}
