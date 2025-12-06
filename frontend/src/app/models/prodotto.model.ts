export interface Prodotto {
  id: number;
  nome: string;
  quantita: number;
  userName: string;
  dataAggiunta: string; 
  acquistato: boolean;
  listaDellaSpesaId: number;
}

export interface CreateProdottoDto {
  nome: string;
  quantita: number;
  userName: string;
  listaDellaSpesaId: number;
}

export interface UpdateProdottoDto {
  nome: string;
  quantita: number;
  userName: string;
  acquistato: boolean;
}
