import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'; // ⬅️ IMPORT HttpParams
import { Observable } from 'rxjs';
import { ListaDellaSpesa, CreateListaDto, UpdateListaDto } from '../models/lista.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ListaService {
  private apiUrl = environment.apiUrl + 'ListeDellaSpesa';

  constructor(private http: HttpClient) {}

  getAllListe(
    orderBy: string = 'DataCreazione',
    ascending: boolean = false,
    creataDa: string | null = null,
    conclusa: boolean | null = null
  ): Observable<ListaDellaSpesa[]> {
    let params = new HttpParams().set('orderBy', orderBy).set('ascending', ascending.toString());

    if (creataDa) {
      params = params.set('creataDa', creataDa);
    }

    if (conclusa !== null) {
      params = params.set('conclusa', conclusa.toString());
    }

    console.log('📡 Chiamata API con params:', params.toString());

    return this.http.get<ListaDellaSpesa[]>(this.apiUrl, { params });
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
