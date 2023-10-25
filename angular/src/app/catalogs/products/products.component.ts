import {PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { ProductsDetailComponent } from './products-detail.component';
import { NotificationService } from '../../shared/services/notification.service';
import { ProductType } from '@proxy/tedu-ecommance/products';
import { ConfirmationService } from 'primeng/api';
import { ProductsAttributeComponent } from './products-attribute.component';
import { ProductsDto, ProductsInListDto, ProductsService } from '@proxy/catalogs/products';
import { ProductCategoryInListDto, ProductsCategoriesService } from '@proxy/catalogs/product-categories';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductsComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>(); 
   blockedPanel:boolean = false
   items: ProductsInListDto[] = [];
   public selectedItem: ProductsInListDto[] = [];

//pagi
   public skipCount: number = 0;
   public maxResultCount: number = 10;
   public totalCount:number;

   //filter
   productCategories: any[] = [];
   keyword: string = '';
   categoryId: string = '';
 

   constructor(
    private productService: ProductsService, 
    private productCategoryService: ProductsCategoriesService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
    ) {}


  ngOnDestroy(): void {
    this.ngUnsubscribe.next()
    this.ngUnsubscribe.complete()
  }

  ngOnInit(): void {
    this.loadProductCategories();
    this.loadData();
    console.log(this.items)
  }



  loadData() {
    this.toggleBlockUI(true);
    this.productService
      .getListFilter({
        keyword: this.keyword,
        categoryId: this.categoryId,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<ProductsInListDto>) => {
          this.items = response.items;
          this.totalCount = response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  loadProductCategories() {
    this.productCategoryService.getListAll().subscribe((response: ProductCategoryInListDto[]) => {
      response.forEach(element => {
        this.productCategories.push({
          value: element.id,
          label: element.name,
        });
      });
    });
  }
  pageChanged(event: any): void{
    this.skipCount = (event.page - 1) * this.maxResultCount
    this.maxResultCount = event.row
    this.loadData()
  }
  showAddModel(){
    const ref  = this.dialogService.open(ProductsDetailComponent,{
      header:'thêm mới sản phẩm',
      width:'70%'
    });
    ref.onClose.subscribe((data:ProductsDto)=>{
      if(data){
        this.loadData()
        this.notificationService.showSucces('Thêm sản phẩm thành công')
        this.selectedItem=[]
      }
    })
  }

  showEditModel(){
    if(this.selectedItem.length ==0){
      this.notificationService.showError('ban phai chon 1 ban ghi')
      return;
    }
    const id = this.selectedItem[0].id
    const ref  = this.dialogService.open(ProductsDetailComponent,{
      data:{
        id:id
      },
      header:'Cập nhật sản phẩm',
      width:'70%'
    });
    ref.onClose.subscribe((data:ProductsDto)=>{
      if(data){
        this.loadData()
        this.selectedItem=[]
        this.notificationService.showSucces('Thêm sản phẩm thành công')
      }
    })
  }
  deleteItemConfirmed(ids: string[]){
    this.toggleBlockUI(true)
      this.productService.deleteMutiple(ids).pipe(takeUntil(this.ngUnsubscribe)).subscribe({
        next: ()=>{
          this.notificationService.showSucces('xoa thanh cong')
          this.loadData()
          this.selectedItem=[]
          this.toggleBlockUI(false)
        },
        error:()=>{
          this.toggleBlockUI(false)
        }
      })
  }

  deleteItems(){
    if(this.selectedItem.length == 0){
      this.notificationService.showError("Phai chon it nhat 1 ban ghi")
      return;
    }
    var ids = []
    this.selectedItem.forEach(element =>{
      ids.push(element.id);
    })
    this.confirmationService.confirm({
      message:'Ban co muon xoa ban ghi nay ?',
      accept:() =>{
        this.deleteItemConfirmed(ids) 
      }
    })
  }

  getProductTypeName(value: number){
    return ProductType[value];
  }

  private toggleBlockUI(enabled : boolean){
    if(enabled == true){
      this.blockedPanel = true;
    }else{
      setTimeout(() =>{
        this.blockedPanel = false
      },1000)
    }
  }

  manageProductAttribute(id:string){
    const ref  = this.dialogService.open(ProductsAttributeComponent,{
      data:{
        id:id
      },
      header:'Quản lý thuộc tính sản phẩm',
      width:'70%'
    });
    ref.onClose.subscribe((data:ProductsDto)=>{
      if(data){
        this.loadData()
        this.selectedItem=[]
        this.notificationService.showSucces('Cập nhật thuộc tính thành công')
      }
  });
  }
}
