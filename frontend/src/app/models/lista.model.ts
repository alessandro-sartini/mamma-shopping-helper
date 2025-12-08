import { Prodotto } from './prodotto.model';

export interface ListaDellaSpesa {
  id: number;
  titolo: string;
  descrizione?: string;
  dataCreazione: string;
  dataUltimaModifica: string;
  conclusa: boolean;
  creataDa: string;          
  prodotti: Prodotto[];
}

export interface CreateListaDto {
  titolo: string;
  descrizione?: string;
  creataDa?: string; 

}

export interface UpdateListaDto {
  titolo: string;
  descrizione?: string;
  conclusa: boolean;
}
