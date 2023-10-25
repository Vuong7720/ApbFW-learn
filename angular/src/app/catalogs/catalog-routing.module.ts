import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { AttributeComponent } from './productAttributes/Attribute.component';

const routes: Routes = [
  { path: 'products', component: ProductsComponent },
  { path: 'attribute', component: AttributeComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogRoutingModule {}
