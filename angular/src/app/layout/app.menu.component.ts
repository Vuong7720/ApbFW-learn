import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];

    constructor(public layoutService: LayoutService) { }

    ngOnInit() {
        this.model = [
            {
                label: 'Trang chủ',
                items: [
                    { label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/'] }
                ]
            },
            {
                label: 'Sản phẩm',
                items: [

                    {
                        label: 'Danh sách sản phẩm', icon: 'pi pi-fw pi-bookmark',
                        routerLink: ['/catalog/products'],
                        permissions:'Tedu_EcomanceCatalog.Product'
                    },
                ]
            },
            {
                label: 'Thuộc tính',
                items: [

                    {
                        label: 'Danh sách thuộc tính', icon: 'pi pi-fw pi-circle',
                        routerLink: ['/catalog/attribute'],
                        permissions:'Tedu_EcomanceCatalog.Attribute'
                    },
                ]
            },
            {
                label: 'Hệ thống',
                items: [

                    {
                        label: 'Danh sách quyền', icon: 'pi pi-fw pi-circle',
                        routerLink: ['/system/role'],
                        permissions:'AbpIdentity.Roles'
                    },
                    {
                        label: 'Danh sách người dùng', icon: 'pi pi-fw pi-circle',
                        routerLink: ['/system/user'],
                        permissions:'AbpIdentity.Users'
                    },
                ]
            },


        ]
    }
}
