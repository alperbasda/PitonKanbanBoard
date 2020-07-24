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

  loginUser: PostLoginModel;
  loginUserForm: FormGroup;

  constructor(private authService: AuthService, private formBuilder: FormBuilder) { }


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
    debugger;
    if (this.loginUserForm.valid) {
      this.loginUser = Object.assign({},this.loginUserForm.value);
      this.authService.login(this.loginUser);
    }
      
  }


}
