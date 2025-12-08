import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ListaService } from '../../../services/lista';
import { ListaDellaSpesa } from '../../../models/lista.model';
import { ProdottoService } from '../../../services/prodotto';
import { CreateProdottoDto } from '../../../models/prodotto.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-lista-detail',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './lista-detail.html',
  styleUrls: ['./lista-detail.css'],
})
export class ListaDetail implements OnInit {
  lista: ListaDellaSpesa | null = null;
  loading = true;
  error = '';

  // ====== VARIABILI FORM =========
  nuovoProdottoNome = '';
  nuovoProdottoQuantita = 1;
  // ==============================

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private listaService: ListaService,
    private prodottoService: ProdottoService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      const listaId = parseInt(id, 10);
      this.loadLista(listaId);
    } else {
      this.error = 'ID lista non valido';
      this.loading = false;
    }
  }

  loadLista(id: number): void {
    this.loading = true;

    this.listaService.getListaById(id).subscribe({
      next: (data) => {
        this.lista = data;
        this.loading = false;
        console.log('Lista dettaglio caricata:', data);
      },
      error: (err) => {
        this.error = 'Errore caricamento lista';
        this.loading = false;
        console.error('Errore:', err);
      },
    });
  }

  goBack(): void {
    this.router.navigate(['/']);
  }

  editLista(): void {
    if (this.lista) {
      this.router.navigate(['/lista', this.lista.id, 'edit']);
    }
  }

  // ========== PRODOTTI ==========

  deleteProdotto(prodottoId: number, event: Event): void {
    event.stopPropagation();

    if (!confirm('Vuoi eliminare questo prodotto?')) return;

    this.prodottoService.deleteProdotto(prodottoId).subscribe({
      next: () => {
        console.log('Prodotto eliminato:', prodottoId);
        if (this.lista) {
          this.loadLista(this.lista.id);
        }
      },
      error: (err) => {
        alert('Errore eliminazione prodotto');
        console.error('Errore:', err);
      },
    });
  }

  toggleAcquistato(prodottoId: number, event: Event): void {
    event.stopPropagation();

    this.prodottoService.toggleAcquistato(prodottoId).subscribe({
      next: () => {
        console.log('Toggle acquistato:', prodottoId);
        if (this.lista) {
          this.loadLista(this.lista.id);
        }
      },
      error: (err) => {
        alert('Errore toggle acquistato');
        console.error('Errore:', err);
      },
    });
  }

  incrementaQuantita(prodottoId: number, event: Event): void {
    event.stopPropagation();

    this.prodottoService.incrementaQuantita(prodottoId, 1).subscribe({
      next: () => {
        console.log(' Incrementato +1:', prodottoId);
        if (this.lista) {
          this.loadLista(this.lista.id);
        }
      },
      error: (err) => {
        alert('Errore incremento quantità');
        console.error(' Errore:', err);
      },
    });
  }

  decrementaQuantita(prodottoId: number, event: Event): void {
    event.stopPropagation();

    this.prodottoService.decrementaQuantita(prodottoId, 1).subscribe({
      next: () => {
        console.log('Decrementato -1:', prodottoId);
        if (this.lista) {
          this.loadLista(this.lista.id);
        }
      },
      error: (err) => {
        alert('Errore decremento quantità');
        console.error(' Errore:', err);
      },
    });
  }

  getProdottiAcquistati(): number {
    if (!this.lista) return 0;
    return this.lista.prodotti.filter((p) => p.acquistato).length;
  }

  getProgressPercent(): number {
    if (!this.lista || this.lista.prodotti.length === 0) return 0;
    return Math.round((this.getProdottiAcquistati() / this.lista.prodotti.length) * 100);
  }

  // ======CREATE PRODOTTO==========
  aggiungiProdotto(): void {
    if (!this.nuovoProdottoNome.trim()) {
      alert('Inserisci il nome del prodotto');
      return;
    }

    if (!this.lista) return;

    // userName da localStorage
    const userName = localStorage.getItem('shopping_userName') || 'Guest';

    // Crea DTO
    const dto: CreateProdottoDto = {
      nome: this.nuovoProdottoNome.trim(),
      quantita: this.nuovoProdottoQuantita,
      userName: userName,
      listaDellaSpesaId: this.lista.id,
    };

    this.prodottoService.createProdotto(dto).subscribe({
      next: (nuovoProdotto) => {
        console.log('Prodotto creato:', nuovoProdotto);

        // Reset form
        this.nuovoProdottoNome = '';
        this.nuovoProdottoQuantita = 1;

        if (this.lista) {
          this.loadLista(this.lista.id);
        }
      },
      error: (err) => {
        alert('Errore creazione prodotto');
        console.error('Errore:', err);
      },
    });
  }
}
