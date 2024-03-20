import { IdentityGuard } from './identity/identity.guard';
import { Routes, mapToCanActivate } from '@angular/router';
import { LayoutComponent } from './pages/layout/layout.component';
import { SignInComponent } from './pages/signin/sign-in.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';
import { AppComponent } from './app.component';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'signin'
    },
    {
        path: '',
        canActivateChild: mapToCanActivate([IdentityGuard]),
        component: LayoutComponent,
        children: [
            {
                path: 'home',
                component: HomeComponent
            },
        ]
    },
    {
        path: 'signin',
        component: SignInComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    }
];
