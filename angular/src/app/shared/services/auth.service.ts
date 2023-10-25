import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ACCESS_TOKEN, REFRESH_TOKEN } from '../constants/keys.const';
import { LoginRequestDto } from '../models/login_request.dto';
import { LoginResponsetDto } from '../models/login_response.dto';
import { TokenStorageService } from './token.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private httpClient: HttpClient, private tokenStorageService: TokenStorageService) {}
  public login(input: LoginRequestDto): Observable<LoginResponsetDto> {
    var body = {
      username: input.username,
      password: input.password,
      client_id: environment.oAuthConfig.clientId,
      client_secret: environment.oAuthConfig.dummyClientSecret,
      grant_type: 'password',
      scope: environment.oAuthConfig.scope
    };

    const data = Object.keys(body).map((key, index) => `${key}=${encodeURIComponent(body[key])}`).join('&');
    return this.httpClient.post<LoginResponsetDto>(
      environment.oAuthConfig.issuer + 'connect/token',
      data,
      { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }
    );
  }

  public refreshToken(refreshToken: string): Observable<LoginResponsetDto> {
    var body = {
      client_id: environment.oAuthConfig.clientId,
      client_secret: environment.oAuthConfig.dummyClientSecret,
      grant_type: 'password',
    };

    const data = Object.keys(body).map((key, index) => `${key}=${encodeURIComponent(body[key])}`).join('&');
    return this.httpClient.post<LoginResponsetDto>(
      environment.oAuthConfig.issuer + 'connect/token',
      data,
      { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }
    );
  }

  public isAuthenticated(): boolean{
    return this.tokenStorageService.getToken() != null;
  }

  public logout(){
    this.tokenStorageService.signOut()

  }
}