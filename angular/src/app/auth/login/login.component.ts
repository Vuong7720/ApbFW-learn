import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { ACCESS_TOKEN, REFRESH_TOKEN } from 'src/app/shared/constants/keys.const';
import { AuthService } from 'src/app/shared/services/auth.service';
import { LoginRequestDto } from 'src/app/shared/models/login_request.dto';
import { TokenStorageService } from 'src/app/shared/services/token.service';
import { LoginResponsetDto } from 'src/app/shared/models/login_response.dto';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
    `
      :host ::ng-deep .p-password input {
        width: 100%;
        padding: 1rem;
      }

      :host ::ng-deep .pi-eye {
        transform: scale(1.6);
        margin-right: 1rem;
        color: var(--primary-color) !important;
      }

      :host ::ng-deep .pi-eye-slash {
        transform: scale(1.6);
        margin-right: 1rem;
        color: var(--primary-color) !important;
      }
    `,
  ],
})

export class LoginComponent implements OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  valCheck: string[] = ['remember'];

  password!: string;

  loginForm: FormGroup;
  public blockedPanel:boolean = false

  constructor(
    public layoutService: LayoutService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private tokenService: TokenStorageService,
    private notificationService:NotificationService
    
  ) {
    this.loginForm = this.fb.group({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  login() {
    this.toggleBlockUI(true)
    var request: LoginRequestDto = {
      username: this.loginForm.controls['username'].value,
      password: this.loginForm.controls['password'].value,
    };
    this.authService
      .login(request)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next:(res:LoginResponsetDto) =>{
            this.tokenService.saveToken(res.access_token);
            this.tokenService.saveRefreshToken(res.refresh_token);
            this.toggleBlockUI(false)
            this.router.navigate(['']);
        },
        error:(ex) =>{
          this.notificationService.showError('Sai đăng nhập')
          this.toggleBlockUI(false)
        }
      });
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

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}