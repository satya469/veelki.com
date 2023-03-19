import { Injectable } from '@angular/core';
import { UserData } from '../models/userData';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor() { }

  setSession(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }
  setSessionStorage(key: string, value: any): void {
    sessionStorage.setItem(key, JSON.stringify(value));
  }
  removeSession(key: string): void {
    localStorage.removeItem(key);
  }
  getSession(key: string): any {
    if (typeof window !== 'undefined') {
      const retrievedObject = localStorage.getItem(key) as string;
      return JSON.parse(retrievedObject);
    }
  }

  getSessionStorage(key: string): any {
    if (typeof window !== 'undefined') {
      const retrievedObject = sessionStorage.getItem(key) as string;
      return retrievedObject;
    }
  }

  clearSession(): void {
    let version = this.getSession('app-version');
    localStorage.clear();
    if (version) {
      this.setSession('app-version', JSON.parse(version));
    }
  }

  getLoggedInUser(): UserData {
    const user = this.getSession('USER_DATA');
    return user ? user : null;
  }

}
