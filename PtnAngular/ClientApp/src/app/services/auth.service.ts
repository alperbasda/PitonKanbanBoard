import {Injectable, Inject} from '@angular/core';
import { PostLoginModel, DataResponse } from "../../models/models";
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthService
{
  
  private apiUrl: string;
  userToken: any;
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiUrl = baseUrl;
  }

  login(loginUser: PostLoginModel) {
    debugger;
    this.httpClient.post<DataResponse>(this.apiUrl + "/api/Auth/login", loginUser).subscribe(
      data => {
        if (data.Success) {
          this.saveToken(data.Data.toString());
          this.userToken = data.Data;
          alert("Giriş Başarılı");
        } else {
          alert("Kullanıcı Adı veya Şifre Hatalı");
        }

      }
    );
  }

  saveToken(value: string) {
    localStorage.setItem('Authorize', value);
  }
}
