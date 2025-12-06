import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';  // ⬅️ AGGIUNGI Router
import { ListaService } from '../../../services/lista';
import { ListaDellaSpesa } from '../../../models/lista.model';

@Component({
  selector: 'app-liste-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './liste-list.html',
  styleUrls: ['./liste-list.css']
})
export class ListeList implements OnInit {
  liste: ListaDellaSpesa[] = [];
  loading = true;
  error = '';

  constructor(
    private listaService: ListaService,
    private router: Router  // ⬅️ AGGIUNGI Router
  ) {}

  ngOnInit(): void {
    this.loadListe();
  }

  loadListe(): void {
    this.loading = true;
    
    this.listaService.getAllListe().subscribe({
      next: (data) => {
        this.liste = data;
        this.loading = false;
        console.log('✅ Liste caricate:', data);
      },
      error: (err) => {
        this.error = 'Errore caricamento liste';
        this.loading = false;
        console.error('❌ Errore:', err);
      }
    });
  }

  
  editLista(id: number, event: Event): void {
    event.stopPropagation();
    this.router.navigate(['/lista', id, 'edit']);
  }

  deleteLista(id: number, event: Event): void {
    event.stopPropagation(); 
    
    if (!confirm('Vuoi eliminare questa lista?')) return;

    this.listaService.deleteLista(id).subscribe({
      next: () => {
        console.log('✅ Lista eliminata:', id);
        this.loadListe(); 
      },
      error: (err) => {
        alert('Errore eliminazione lista');
        console.error('❌ Errore:', err);
      }
    });
  }

  toggleConclusa(id: number, event: Event): void {
    event.stopPropagation();

    this.listaService.toggleConclusa(id).subscribe({
      next: () => {
        console.log('✅ Lista conclusa toggled:', id);
        this.loadListe();
      },
      error: (err) => {
        alert('Errore aggiornamento lista');
        console.error('❌ Errore:', err);
      }
    });
  }

  getProdottiAcquistati(lista: ListaDellaSpesa): number {
    return lista.prodotti.filter(p => p.acquistato).length;
  }

  getProgressPercent(lista: ListaDellaSpesa): number {
    if (lista.prodotti.length === 0) return 0;
    return Math.round((this.getProdottiAcquistati(lista) / lista.prodotti.length) * 100);
  }
}
