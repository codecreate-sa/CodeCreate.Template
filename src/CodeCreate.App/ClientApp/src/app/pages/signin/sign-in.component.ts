import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { IdentityService } from '../../identity/identity.service';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';

import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    RouterModule
  ],
  providers: [IdentityService, FormBuilder, Router],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss'
})
export class SignInComponent implements OnInit {

  loginForm!: FormGroup;
  authFailed: boolean = false;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: IdentityService
  ) {
    this.authService.isSignedIn()
      .subscribe(
        isSignedIn => {
          if (isSignedIn) {
            this.router.navigate(['home']);
          }
        });
  }

  ngOnInit(): void {
    this.authFailed = false;
    this.loginForm = this.formBuilder.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required]]
      });
  }

  public signIn(_: any) {

    if (!this.loginForm.valid) {
      return;
    }

    const userName = this.loginForm.get('email')?.value;
    const password = this.loginForm.get('password')?.value;

    this.authService.signIn(userName, password).forEach(
      response => {
        if (response) {
          this.router.navigate(['home']);
        }
      }).catch(
        _ => {
          this.authFailed = true;
        });
  }
}
