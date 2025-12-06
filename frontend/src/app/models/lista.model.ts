import { Prodotto } from './prodotto.model';

export interface ListaDellaSpesa {
  id: number;
  titolo: string;
  descrizione?: string;
  dataCreazione: string;
  conclusa: boolean;
  prodotti: Prodotto[];
}

export interface CreateListaDto {
  titolo: string;
  descrizione?: string;
}

export interface UpdateListaDto {
  titolo: string;
  descrizione?: string;
  conclusa: boolean;
}


