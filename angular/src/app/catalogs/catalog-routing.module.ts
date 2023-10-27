import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { AttributeComponent } from './productAttributes/Attribute.component';
import { PermissionGuard } from '@abp/ng.core';

const routes: Routes = [
  { path: 'products', component: ProductsComponent ,canActivate:[PermissionGuard], data:{
    requiredPolicy:'Tedu_EcomanceCatalog.Product',
  } },
  { path: 'attribute', component: AttributeComponent,canActivate:[PermissionGuard], data:{
    requiredPolicy:'Tedu_EcomanceCatalog.Attribute',
  } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule {}
