import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Prodotto, CreateProdottoDto, UpdateProdottoDto } from '../models/prodotto.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProdottoService {
  private apiUrl = environment.apiUrl + 'Prodotti';

  constructor(private http: HttpClient) {}

  // CREATE
  createProdotto(dto: CreateProdottoDto): Observable<Prodotto> {
    return this.http.post<Prodotto>(this.apiUrl, dto);
  }

  // UPDATE
  updateProdotto(id: number, dto: UpdateProdottoDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }

  // DELETE
  deleteProdotto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  // TOGGLE ACQUISTATO
  toggleAcquistato(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/acquistato`, {}); 
  }

  // INCREMENTA QUANTITÀ
  incrementaQuantita(id: number, quantita: number = 1): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/incrementa-quantita?quantita=${quantita}`, {});
  }

  // DECREMENTA QUANTITÀ
  decrementaQuantita(id: number, quantita: number = 1): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/decrementa-quantita?quantita=${quantita}`, {});
  }
}
