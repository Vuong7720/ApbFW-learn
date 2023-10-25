import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from '../../shared/services/Utility.service';
import { attributeTypeOptions } from '@proxy/tedu-ecommance/product-attributes';
import { NotificationService } from '../../shared/services/notification.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ProductAttributeDto, ProductAttributeInListDto } from '@proxy/catalogs/product-attributes/models';
import { ProductAttributeService } from '@proxy/catalogs/product-attributes/product-attribute.service';


@Component({
  selector: 'app-Attribute-detail',
  templateUrl: './Attribute-detail.component.html',
})
export class AttributeDetailComponent  implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>(); 
   blockedPanel:boolean = false
   items: ProductAttributeInListDto[] = [];


   public form: FormGroup;
   //dropdow
   attributeTypes:any[] = [];
   btnDisabled = false;
   selectedEntity = {} as ProductAttributeDto;
 

   constructor(private AttributeService: ProductAttributeService,
     private fb:FormBuilder,
     private config: DynamicDialogConfig,
     private ref: DynamicDialogRef,
     private utilService: UtilityService,
     private notificationService: NotificationService,
     private cd: ChangeDetectorRef,
     private sanitizer:DomSanitizer
     ) {}

     validationMessages = {
      lable: [
        { type: 'required', message: 'Bạn phải nhập tên' },
        { type: 'maxlength', message: 'Bạn không được nhập quá 50 kí tự' },
      ],
      attributeType: [{ type: 'required', message: 'Bạn phải chọn loại thuộc tính' }],
      sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự' }],
    };
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
    this.loadAttributeType();
    this.initFormData();
    
  }

  initFormData() {
    //Load edit data to form
    if (this.utilService.isEmpty(this.config.data?.id) == true) {
      this.toggleBlockUI(false);
    } else {
      this.loadFormDetail(this.config.data?.id);
    }
  }

 
  saveChange(){
    this.toggleBlockUI(true);
    if(this.utilService.isEmpty(this.config.data?.id)==true){
      this.AttributeService.create(this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next:()=>{
          this.toggleBlockUI(false)
          this.ref.close(this.form.value)
        },
        error:() =>{
          this.toggleBlockUI(false)
        }
      }
      )
    }else{
      this.AttributeService.update(this.config.data?.id,this.form.value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next:()=>{
          this.toggleBlockUI(false)
          this.ref.close(this.form.value)
        },
        error:(err) =>{
          this.notificationService.showError(err.error.error.message);
          this.toggleBlockUI(false)
        }
      })
    }
  }
loadAttributeType(){
  attributeTypeOptions.forEach(element =>{
    this.attributeTypes.push({
      value:element.value,
      lable:element.key,
    })
  })
}


  loadFormDetail(id:string){
    this.toggleBlockUI(true);
    this.AttributeService.get(id).pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: ProductAttributeDto) =>{
        this.selectedEntity = response;
        this.buildForm();
        this.toggleBlockUI(false)
      },
      error(err){
        this.notificationService.showError(err.error.error.message);
        this.toggleBlockUI(false)
      }
    })
  }

  private buildForm() {
    this.form = this.fb.group({
      lable: new FormControl(this.selectedEntity.lable || null, Validators.compose([Validators.required, Validators.maxLength(250)])),
      code: new FormControl(this.selectedEntity.code || null, Validators.required),
     
      attributeType: new FormControl(this.selectedEntity.attributeType || null, Validators.required),
      sortOder: new FormControl(this.selectedEntity.sortOder || null, Validators.required),
      visibility: new FormControl(this.selectedEntity.visibility || true),
      isActive: new FormControl(this.selectedEntity.isActive || true),
      isRequired: new FormControl(this.selectedEntity.isRequired || true,Validators.required),
      isUnique: new FormControl(this.selectedEntity.isUnique || true,Validators.required),
      note: new FormControl(this.selectedEntity.note || null),
    });
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

