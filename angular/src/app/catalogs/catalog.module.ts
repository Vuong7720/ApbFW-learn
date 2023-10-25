import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { CatalogRoutingModule } from './catalog-routing.module';
import { PanelModule } from 'primeng/panel';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';
import { TeduSharedModule } from '../shared/modules/tedu-shared.module';
import {BadgeModule} from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CalendarModule } from 'primeng/calendar';
import { ProductsComponent } from './products/products.component';
import { ProductsDetailComponent } from './products/products-detail.component';
import { ProductsAttributeComponent } from './products/products-attribute.component';
import { AttributeComponent } from './productAttributes/Attribute.component';
import { AttributeDetailComponent } from './productAttributes/Attribute-detail.component';

@NgModule({
  declarations: [ProductsComponent,
    ProductsDetailComponent, 
    ProductsAttributeComponent, 
    AttributeComponent, 
    AttributeDetailComponent],
  imports: [SharedModule,
    CatalogRoutingModule,
    PanelModule,
    PaginatorModule,
    TableModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    TeduSharedModule,
    ImageModule,
    BadgeModule,
    ConfirmDialogModule,
    CalendarModule,
    CatalogRoutingModule
  ],
})
export class CatalogModule { }
