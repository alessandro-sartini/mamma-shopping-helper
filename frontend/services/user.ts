import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly STORAGE_KEY = 'shopping_userName';

  constructor() { }

  // Ottieni nome utente corrente
  getUserName(): string | null {
    return localStorage.getItem(this.STORAGE_KEY);
  }

  // Salva nome utente
  setUserName(userName: string): void {
    localStorage.setItem(this.STORAGE_KEY, userName.trim());
  }

  // Rimuovi nome utente (reset/logout)
  clearUserName(): void {
    localStorage.removeItem(this.STORAGE_KEY);
  }

  // Verifica se utente ha già inserito nome
  hasUserName(): boolean {
    const userName = this.getUserName();
    return userName !== null && userName.trim().length > 0;
  }
}
