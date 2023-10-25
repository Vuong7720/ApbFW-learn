import { AccountConfigModule } from '@abp/ng.account/config';
import { CoreModule } from '@abp/ng.core';
import { registerLocale } from '@abp/ng.core/locale';
import { IdentityConfigModule } from '@abp/ng.identity/config';
import { SettingManagementConfigModule } from '@abp/ng.setting-management/config';
import { TenantManagementConfigModule } from '@abp/ng.tenant-management/config';
import { ThemeLeptonXModule } from '@abp/ng.theme.lepton-x';
import { SideMenuLayoutModule } from '@abp/ng.theme.lepton-x/layouts';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { FeatureManagementModule } from '@abp/ng.feature-management';
import { AbpOAuthModule } from '@abp/ng.oauth';
import { AccountLayoutModule } from '@abp/ng.theme.lepton-x/account';
import { AppLayoutModule } from './layout/app.layout.module';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from './shared/services/notification.service';
import { MessageService } from 'primeng/api';
import { UtilityService } from './shared/services/Utility.service';
import {ConfirmationService} from 'primeng/api';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { TokenInterceptor } from './shared/interceptors/token.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { GlobalHttpInterceptorService } from './shared/interceptors/error-handle.interceptors';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppLayoutModule,
    AppRoutingModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    AbpOAuthModule.forRoot(),
    ThemeSharedModule.forRoot(),
    AccountConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
    TenantManagementConfigModule.forRoot(),
    SettingManagementConfigModule.forRoot(),
    
    
    FeatureManagementModule.forRoot(),
    SideMenuLayoutModule.forRoot(),
    ThemeLeptonXModule.forRoot(),
    AccountLayoutModule.forRoot(),
    ConfirmDialogModule,
    ToastModule
  ],
  declarations: [AppComponent],
  providers: [
    {
      provide:HTTP_INTERCEPTORS,
      useClass:TokenInterceptor,
      multi:true
    },
    {
      provide:HTTP_INTERCEPTORS,
      useClass:GlobalHttpInterceptorService,
      multi:true
    },
    APP_ROUTE_PROVIDER,
    DialogService, 
    MessageService, 
    NotificationService,
    UtilityService,
    ConfirmationService],
  bootstrap: [AppComponent],
})
export class AppModule {}
