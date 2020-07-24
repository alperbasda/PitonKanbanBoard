import {Injectable, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PostLoginModel, DataResponse } from "../models/models";
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class AuthService
{
  private apiUrl: string;
  userToken: any;
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string,private router:Router) {
    this.apiUrl = baseUrl;
  }

  login(loginUser: PostLoginModel){
    debugger;
    this.httpClient.post<DataResponse>(this.apiUrl + "api/Auth/login", loginUser).subscribe(
      data => {
        debugger;
        if (data.success) {
          this.saveToken(data.message);
          this.userToken = data.message;
          this.router.navigate(['/event']);
        } else {
          alert("Kullanıcı Adı veya Şifre Hatalı");
        }

      }
    );
  }

  register(loginUser: PostLoginModel) {
    debugger;
    this.httpClient.post<DataResponse>(this.apiUrl + "api/Auth/register", loginUser).subscribe(
      data => {
        debugger;
        if (data.success) {
          this.saveToken(data.message);
          this.userToken = data.message;
          this.router.navigate(['/event']);
        } else {
          alert("Kullanıcı Adı veya Şifre Hatalı");
        }

      }
    );
  }

  saveToken(value: string) {
    localStorage.setItem('Authorize', value);
  }
  getToken() : string {
    return localStorage.getItem('Authorize');
  }
}
