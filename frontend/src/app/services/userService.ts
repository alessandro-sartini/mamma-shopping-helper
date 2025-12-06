import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly STORAGE_KEY = 'shopping_userName';

  constructor() { }

  getUserName(): string | null {
    return localStorage.getItem(this.STORAGE_KEY);
  }

  setUserName(userName: string): void {
    localStorage.setItem(this.STORAGE_KEY, userName.trim());
  }

  clearUserName(): void {
    localStorage.removeItem(this.STORAGE_KEY);
  }

  hasUserName(): boolean {
    const userName = this.getUserName();
    return userName !== null && userName.trim().length > 0;
  }
}
