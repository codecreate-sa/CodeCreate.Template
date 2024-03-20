import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IdentityService } from '../../identity/identity.service';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';

import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule, 
    CardModule, 
    InputTextModule, 
    ButtonModule
  ],
  providers: [IdentityService, FormBuilder, Router],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

  errors: string[] = [];
  registerForm!: FormGroup;
  registerFailed: boolean = false;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: IdentityService
  ) { }

  ngOnInit(): void {
    this.errors = [];
    this.registerForm = this.formBuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required]],
        confirmPassword: ['', [Validators.required]]
      });
  }

  public register(_: any) {
    if (!this.registerForm.valid) {
      return;
    }
    this.errors = [];
    const userName = this.registerForm.get('email')?.value;
    const password = this.registerForm.get('password')?.value;
    const confirmPassword = this.registerForm.get('confirmPassword')?.value;

    if (password !== confirmPassword) {
      this.registerFailed = true;
      this.errors.push('Passwords do not match.');
      return;
    }

    this.authService.register(userName, password).forEach(
      response => {
        if (response) {
          this.router.navigate(['signin']);
        }
      }).catch(
        error => {
          this.registerFailed = true;
          if (error.error) {
            const errorObj = JSON.parse(error.error);
            if (errorObj && errorObj.errors) {
              const errorList = errorObj.errors;
              for (let field in errorList) {
                if (Object.hasOwn(errorList, field)) {
                  let list: string[] = errorList[field];
                  for (let idx = 0; idx < list.length; idx += 1) {
                    this.errors.push(`${field}: ${list[idx]}`);
                  }
                }
              }
            }
          }
        });
  }
}
