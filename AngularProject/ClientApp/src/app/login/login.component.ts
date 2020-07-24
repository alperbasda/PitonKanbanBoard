import { Component, OnInit } from '@angular/core';
import { AuthService } from "../services/auth.service";
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

import { PostLoginModel } from "../models/models";

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  providers: [AuthService]
})
export class LoginComponent implements OnInit {

  
  constructor(private authService: AuthService, private formBuilder: FormBuilder) { }

  loginUser: PostLoginModel;
  loginUserForm: FormGroup;


  ngOnInit(): void {
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginUserForm = this.formBuilder.group
      ({
        EMail: ["", Validators.required],
        Password: ["", Validators.required],
      });
  }

  login() {
    
    if (this.loginUserForm.valid) {
      let token = this.authService.getToken();
      if (token != null && token.length > 0) {
        alert("Zaten Giriş Yapmışsınız");
      }
      this.loginUser = Object.assign({},this.loginUserForm.value);
      this.authService.login(this.loginUser);
    }
      
  }
  register() {

    if (this.loginUserForm.valid) {
      let token = this.authService.getToken();
      if (token != null && token.length > 0) {
        alert("Zaten Giriş Yapmışsınız");
      }
      this.loginUser = Object.assign({}, this.loginUserForm.value);
      this.authService.register(this.loginUser);
    }

  }


}
