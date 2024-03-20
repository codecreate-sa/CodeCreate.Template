import { Router } from "@angular/router";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { IdentityService } from "./identity.service";

@Injectable({ providedIn: 'root' })
// protects routes from unauthenticated users
export class IdentityGuard {

  constructor(
    private authService: IdentityService,
    private router: Router
  ) { }

  canActivate() {
    return this.isSignedIn();
  }

  isSignedIn(): Observable<boolean> {
    return this.authService.isSignedIn().pipe(
      map((isSignedIn) => {
        if (!isSignedIn) {          
          // redirect to signin page
          this.router.navigate(['signin']);
          return false;
        }
        return true;
      }));
  }
}
