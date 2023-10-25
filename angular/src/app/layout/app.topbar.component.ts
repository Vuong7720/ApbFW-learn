import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { LayoutService } from "./service/app.layout.service";
import { AuthService } from '@abp/ng.core';
import { Router } from '@angular/router';
import { LOGIN_URL } from '../shared/constants/urls.const';

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html'
})
export class AppTopBarComponent implements OnInit {

    items!: MenuItem[];
    userMenuItems: MenuItem[]

    @ViewChild('menubutton') menuButton!: ElementRef;

    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

    @ViewChild('topbarmenu') menu!: ElementRef;

    constructor(public layoutService: LayoutService, private authService: AuthService, private router: Router) { }
    ngOnInit(): void {
        this.userMenuItems = [
            {
                label: 'profile',
                icon: 'pi pi-fw pi-file',
                items: [
                    {
                        label: 'New',
                        icon: 'pi pi-id-card',
                        routerLink:['/profile']
                    },
                    {
                        label: 'change password',
                        icon: 'pi pi-fw pi-trash',
                        routerLink:['/change-password']
                    },
                    {
                        label: 'logout',
                        icon: 'pi pi-key',
                        command: event =>{
                            this.authService.logout();
                            this.router.navigate([LOGIN_URL])
                        }
                    }
                ]
            },
        ]
    }
}
