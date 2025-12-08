import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ListaService } from '../../../services/lista';
import { CreateListaDto, UpdateListaDto } from '../../../models/lista.model';

@Component({
  selector: 'app-lista-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './lista-form.html',
  styleUrls: ['./lista-form.css']
})
export class ListaForm implements OnInit {
  isEditMode = false;
  listaId: number | null = null;

  titolo = '';
  descrizione = '';
  conclusa = false;

  loading = false;
  error = '';

  constructor(
    private listaService: ListaService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Controlla se siamo in edit mode
    const id = this.route.snapshot.paramMap.get('id');
    
    if (id && id !== 'new') {
      this.isEditMode = true;
      this.listaId = parseInt(id);
      this.loadLista();
    }
  }

  loadLista(): void {
    if (!this.listaId) return;

    this.loading = true;

    this.listaService.getListaById(this.listaId).subscribe({
      next: (lista) => {
        this.titolo = lista.titolo;
        this.descrizione = lista.descrizione || '';
        this.conclusa = lista.conclusa;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Errore caricamento lista';
        this.loading = false;
        console.error('Errore:', err);
      }
    });
  }

  onSubmit(): void {
    // Validazione
    if (!this.titolo || this.titolo.trim().length < 2) {
      this.error = 'Il titolo deve essere almeno 2 caratteri';
      return;
    }

    this.loading = true;
    this.error = '';

    if (this.isEditMode) {
      this.updateLista();
    } else {
      this.createLista();
    }
  }

  createLista(): void {
    const dto: CreateListaDto = {
      titolo: this.titolo.trim(),
      descrizione: this.descrizione.trim() || undefined
    };

    this.listaService.createLista(dto).subscribe({
      next: (nuovaLista) => {
        console.log('Lista creata:', nuovaLista);
        this.router.navigate(['/']); 
      },
      error: (err) => {
        this.error = 'Errore creazione lista';
        this.loading = false;
        console.error('Errore:', err);
      }
    });
  }

  updateLista(): void {
    if (!this.listaId) return;

    const dto: UpdateListaDto = {
      titolo: this.titolo.trim(),
      descrizione: this.descrizione.trim() || undefined,
      conclusa: this.conclusa
    };

    this.listaService.updateLista(this.listaId, dto).subscribe({
      next: () => {
        console.log('Lista aggiornata');
        this.router.navigate(['/']); 
      },
      error: (err) => {
        this.error = 'Errore aggiornamento lista';
        this.loading = false;
        console.error('Errore:', err);
      }
    });
  }

  onCancel(): void {
    this.router.navigate(['/']);
  }
}
