import {PagedResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import {AttributeDetailComponent } from './Attribute-detail.component';
import { NotificationService } from '../../shared/services/notification.service';
import { ConfirmationService } from 'primeng/api';
import { AttributeType } from '@proxy/tedu-ecommance/product-attributes';
import { ProductAttributeDto } from '@proxy/catalogs/product-attributes/models';
import { ProductAttributeService } from '@proxy/catalogs/product-attributes/product-attribute.service';

@Component({
  selector: 'app-products',
  templateUrl: './Attribute.component.html',
  styleUrls: ['./Attribute.component.scss'],
})
export class AttributeComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>(); 
   blockedPanel:boolean = false
   items: ProductAttributeDto[] = [];
   public selectedItem: ProductAttributeDto[] = [];

//pagi
   public skipCount: number = 0;
   public maxResultCount: number = 10;
   public totalCount:number;

   //filter
   productCategories: any[] = [];
   keyword: string = '';
   categoryId: string = '';
 

   constructor(
    private attributeService: ProductAttributeService, 
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService
    ) {}


  ngOnDestroy(): void {
    this.ngUnsubscribe.next()
    this.ngUnsubscribe.complete()
  }

  ngOnInit(): void {
    this.loadData();
  }



  loadData() {
    this.toggleBlockUI(true);
    this.attributeService
      .getListFilter({
        keyword: this.keyword,
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<ProductAttributeDto>) => {
          this.items = response.items;
          this.totalCount = response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  
  pageChanged(event: any): void{
    this.skipCount = (event.page - 1) * this.maxResultCount
    this.maxResultCount = event.row
    this.loadData()
  }
  showAddModel(){
    const ref  = this.dialogService.open(AttributeDetailComponent,{
      header:'thêm mới thuộc tính',
      width:'70%'
    });
    ref.onClose.subscribe((data:ProductAttributeDto)=>{
      if(data){
        this.loadData()
        this.notificationService.showSucces('Thêm thuộc tính thành công')
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
    const ref  = this.dialogService.open(AttributeDetailComponent,{
      data:{
        id:id
      },
      header:'Cập nhật thuộc tính',
      width:'70%'
    });
    ref.onClose.subscribe((data:ProductAttributeDto)=>{
      if(data){
        this.loadData()
        this.selectedItem=[]
        this.notificationService.showSucces('Thêm thuộc tính thành công')
      }
    })
  }
  deleteItemConfirmed(ids: string[]){
    this.toggleBlockUI(true)
      this.attributeService.deleteMutiple(ids).pipe(takeUntil(this.ngUnsubscribe)).subscribe({
        next: ()=>{
          this.notificationService.showSucces('Xóa thành công')
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
      this.notificationService.showError("Phải chọn ít nhất 1 bản ghi")
      return;
    }
    var ids = []
    this.selectedItem.forEach(element =>{
      ids.push(element.id);
    })
    this.confirmationService.confirm({
      message:'Bạn có muốn xóa bản ghi này ?',
      accept:() =>{
        this.deleteItemConfirmed(ids) 
      }
    })
  }

  getAttributeTypeName(value: number){
    return AttributeType[value];
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
}
