import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Prodotto, CreateProdottoDto, UpdateProdottoDto } from '../models/prodotto.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProdottoService {
  private apiUrl = environment.apiUrl+"Prodotti";

  constructor(private http: HttpClient) {}

  createProdotto(dto: CreateProdottoDto): Observable<Prodotto> {
    return this.http.post<Prodotto>(this.apiUrl, dto);
  }

  updateProdotto(id: number, dto: UpdateProdottoDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }

  deleteProdotto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  toggleAcquistato(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/toggle-acquistato`, {});
  }

  incrementaQuantita(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/incrementa-quantita`, {});
  }

  decrementaQuantita(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/decrementa-quantita`, {});
  }
}
