import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ListaDellaSpesa, CreateListaDto, UpdateListaDto } from '../models/lista.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ListaService {
  private apiUrl = environment.apiUrl+"ListeDellaSpesa";

  constructor(private http: HttpClient) {}

  getAllListe(): Observable<ListaDellaSpesa[]> {
    return this.http.get<ListaDellaSpesa[]>(this.apiUrl);
  }

  getListaById(id: number): Observable<ListaDellaSpesa> {
    return this.http.get<ListaDellaSpesa>(`${this.apiUrl}/${id}`);
  }

  createLista(dto: CreateListaDto): Observable<ListaDellaSpesa> {
    return this.http.post<ListaDellaSpesa>(this.apiUrl, dto);
  }

  updateLista(id: number, dto: UpdateListaDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }

  deleteLista(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  toggleConclusa(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/conclusa`, {});
  }
}
