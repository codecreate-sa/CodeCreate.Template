import { Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IdentityService } from '../../identity/identity.service';
import { UserInfoModel } from '../../identity/models/user-info.model';

import { MenuModule } from 'primeng/menu';
import { ButtonModule } from 'primeng/button';
import { MenubarModule } from 'primeng/menubar';
import { MenuItem, PrimeIcons } from 'primeng/api';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    CommonModule,
    MenubarModule,
    ButtonModule,
    MenuModule
  ],
  providers: [IdentityService, Router],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

  isSignedIn: boolean = false;
  loggedInUser$!: Observable<UserInfoModel>;

  constructor(
    private auth: IdentityService,
    private router: Router
  ) {
    this.loggedInUser$ = this.auth.user()
      .pipe(tap((user: UserInfoModel) => {
        if (user === null) {
          this.isSignedIn = false;
          return;
        }
        this.isSignedIn = true;
        return user;
      }))
  }

  settingsItems: MenuItem[] = [
    {
      label: 'Profile',
      icon: PrimeIcons.COG,
      routerLink: 'profile'
    },
    {
      label: 'Help',
      icon: PrimeIcons.INFO_CIRCLE,
      routerLink: 'help'
    }];

  signOut() {
    if (this.isSignedIn) {
      this.auth.signOut()
        .subscribe(() => {
          this.router.navigate(['signin']);
        });
    }
  }
}
