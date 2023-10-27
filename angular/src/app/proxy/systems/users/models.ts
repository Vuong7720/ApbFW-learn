import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUserDto {
  name?: string;
  userName?: string;
  password?: string;
  email?: string;
  surname?: string;
  phoneNumber?: string;
}

export interface SetPasswordDto {
  newPassword?: string;
  confirmNewPassword?: string;
}

export interface UpdateUserDto {
  name?: string;
  email?: string;
  surname?: string;
  phoneNumber?: string;
}

export interface UserDto extends AuditedEntityDto<string> {
  name?: string;
  userName?: string;
  password?: string;
  email?: string;
  surname?: string;
  phoneNumber?: string;
  roles: string[];
  isActive: boolean;
}

export interface UserInlistDto extends AuditedEntityDto<string> {
  name?: string;
  userName?: string;
  email?: string;
  surname?: string;
  phoneNumber?: string;
}
