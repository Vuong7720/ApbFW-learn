import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { UtilityService } from '../../shared/services/Utility.service';
import { NotificationService } from '../../shared/services/notification.service';
import { AttributeType } from '@proxy/tedu-ecommance/product-attributes';
import { ConfirmationService } from 'primeng/api';
import { ProductsInListDto } from '@proxy/catalogs/products/models';
import { ProductAttributeService } from '@proxy/catalogs/product-attributes/product-attribute.service';
import { ProductsService } from '@proxy/catalogs/products/products.service';
import { ProductAttributeInListDto } from '@proxy/catalogs/product-attributes/models';
import { ProductAttributeValueDto } from '@proxy/catalogs/products/attributes/models';


@Component({
  selector: 'app-product-attribute',
  templateUrl: './products-attribute.component.html',
})
export class ProductsAttributeComponent  implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>(); 
   blockedPanel:boolean = false
   items: ProductsInListDto[] = [];


   public form: FormGroup;
   //dropdow
   attributes: any[] = [];
   fullAttributes: any[] = [];
   productAttributes:any[] = [];
   btnDisabled = false;
   showDateTimeControl: boolean = false;
   showDecimalControl: boolean = false;
   showIntControl: boolean = false;
   showTextControl: boolean = false;
   showVarcharControl: boolean = false;
 

   constructor(
    private attributeService: ProductAttributeService,
    private productService:ProductsService,
     private fb:FormBuilder,
     private config: DynamicDialogConfig,
     private ref: DynamicDialogRef,
     private utilService: UtilityService,
     private notificationService: NotificationService,
     private confirmationService:ConfirmationService
     ) {}

  ngOnDestroy(): void {
    if(this.ref){
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  ngOnInit(): void {
    this.buildForm()  

    //load data to form
    this.InitFormData();
    
  }

InitFormData(){
  var attributes = this.attributeService.getListAll();
    this.toggleBlockUI(true);
    forkJoin({
      attributes,
    })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: any) => {
          //Push data to dropdown
          this.fullAttributes = response.attributes
          var attributes = response.attributes as ProductAttributeInListDto[];
          attributes.forEach(element => {
            this.attributes.push({
              value: element.id,
              label: element.lable,
            });
          });

          //Load edit data to form
          this.loadFormDetail(this.config.data?.id);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
}

  saveChange(){
    this.toggleBlockUI(true)
      this.productService
      .addAttribute(this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next:()=>{
          this.toggleBlockUI(false)
          this.loadFormDetail(this.config?.data?.id)
        },
        error:() =>{
          this.toggleBlockUI(false)
        }
      }
      )
  }
  loadFormDetail(id:string){
    this.toggleBlockUI(true);
    this.productService
    .getListProductAttributeAll(id)
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: ProductAttributeValueDto[]) =>{
        this.productAttributes = response;
        this.buildForm();
        this.toggleBlockUI(false)
      },
      error(err){
        this.notificationService.showError(err.error.error.message);
        this.toggleBlockUI(false)
      }
    })
  }
  getAttributeTypeName( value: number){
    return AttributeType[value];
  }

  getValueByType(attributeValue:ProductAttributeValueDto,value:number){
    if(attributeValue.attributeType == AttributeType.Date){
      return attributeValue.dateTimeValue
    }else if(attributeValue.attributeType == AttributeType.Decimal){
      return attributeValue.decimalValue
    }else if(attributeValue.attributeType == AttributeType.Int){
      return attributeValue.intValue
    }else if(attributeValue.attributeType == AttributeType.Text){
      return attributeValue.textValue
    }else if(attributeValue.attributeType == AttributeType.Varchar){
      return attributeValue.varcharValue
    }
  }

  private buildForm() {
    
    this.form = this.fb.group({
      productId:new FormControl(this.config?.data?.id),
      attributeId: new FormControl( null, Validators.required),
      dateTimeValue: new FormControl(null),
      decimalValue: new FormControl(null),
      intValue: new FormControl(null),
      textValue: new FormControl(null),
      vacharValue: new FormControl(null),
      
    });
  }
  deleteItemConfirmed(attributeValue:ProductAttributeValueDto,id: string){
    this.toggleBlockUI(true)
      this.productService
      .removeAttribute(attributeValue.attributeId, id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: ()=>{
          this.notificationService.showSucces('xoa thanh cong')
          this.loadFormDetail(this.config.data?.id);
          this.toggleBlockUI(false)
        },
        error:()=>{
          this.toggleBlockUI(false)
        }
      })
  }

  removeItem(attributeValue:ProductAttributeValueDto){
    var id = ''
    if(attributeValue.attributeType == AttributeType.Date){
      id = attributeValue.dateTimeId
    }else if(attributeValue.attributeType == AttributeType.Decimal){
      id = attributeValue.decimalId
    }else if(attributeValue.attributeType == AttributeType.Int){
      id = attributeValue.intId
    }else if(attributeValue.attributeType == AttributeType.Text){
      id = attributeValue.textId
    }else if(attributeValue.attributeType == AttributeType.Varchar){
      id = attributeValue.varcharId
    }
    this.confirmationService.confirm({
      message:'Ban co muon xoa ban ghi nay ?',
      accept:() =>{
        this.deleteItemConfirmed(attributeValue, id) 
      }
    })
  }
  selectAttribute(event:any){
    var attributeType = this.fullAttributes.filter(x => x.id == event.value)[0].attributeType;
    this.showDateTimeControl = false;
    this.showDecimalControl = false;
    this.showIntControl = false;
    this.showTextControl = false;
    this.showVarcharControl = false;
    if (attributeType == AttributeType.Date) {
      this.showDateTimeControl = true;
    } else if (attributeType == AttributeType.Decimal) {
      this.showDecimalControl = true;
    } else if (attributeType == AttributeType.Int) {
      this.showIntControl = true;
    } else if (attributeType == AttributeType.Text) {
      this.showTextControl = true;
    } else if (attributeType == AttributeType.Varchar) {
      this.showVarcharControl = true;
    }
  }
  private toggleBlockUI(enabled : boolean){
    if(enabled == true){
      this.blockedPanel = true;
      this.btnDisabled = true;
    }else{
      setTimeout(() =>{
        this.blockedPanel = false;
        this.btnDisabled = false;
      },1000)
    }
  }
 
}

