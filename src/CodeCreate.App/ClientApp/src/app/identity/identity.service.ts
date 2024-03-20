import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, catchError, map, of } from "rxjs";
import { HttpClient, HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { UserInfoModel } from './models/user-info.model';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {

  constructor(private http: HttpClient) { }

  private _authStateChanged: Subject<boolean> = new BehaviorSubject<boolean>(false);

  onStateChanged() {
    return this._authStateChanged.asObservable();
  }

  // cookie-based login
  signIn(email: string, password: string) {
    return this.http.post(environment.apiUrl + '/login?useCookies=true', {
      email: email,
      password: password
    }, {
      withCredentials: true,
      observe: 'response',
      responseType: 'text'
    })
      .pipe<boolean>(map((res: HttpResponse<string>) => {
        this._authStateChanged.next(res.ok);
        return res.ok;
      }));
  }

  // register new user
  register(email: string, password: string) {
    return this.http.post(environment.apiUrl + '/register', {
      email: email,
      password: password
    }, {
      observe: 'response',
      responseType: 'text'
    })
      .pipe<boolean>(map((res: HttpResponse<string>) => {
        return res.ok;
      }));
  }

  // sign out
  signOut() {
    return this.http.post(environment.apiUrl + '/logout', {}, {
      withCredentials: true,
      observe: 'response',
      responseType: 'text'
    }).pipe<boolean>(map((res: HttpResponse<string>) => {
      if (res.ok) {
        this._authStateChanged.next(false);
      }
      return res.ok;
    }));
  }

  // check if the user is authenticated. the endpoint is protected so 401 if not.
  user() {
    return this.http.get<UserInfoModel>(environment.apiUrl + '/manage/info', {
      withCredentials: true
    }).pipe(
      catchError((_: HttpErrorResponse, __: Observable<UserInfoModel>) => {
        return of({} as UserInfoModel);
      }));
  }

  // is signed in when the call completes without error and the user has an email
  isSignedIn(): Observable<boolean> {
    return this.user().pipe(
      map((userInfo) => {
        const valid = !!(userInfo && userInfo.email && userInfo.email.length > 0);
        return valid;
      }),
      catchError((_) => {
        return of(false);
      }));
  }
}
