import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./components/liste/liste-list/liste-list').then((m) => m.ListeList),
  },
  {
    path: 'lista/new',
    loadComponent: () =>
      import('./components/liste/lista-form/lista-form').then((m) => m.ListaForm),
  },
  {
    path: 'lista/:id/edit',
    loadComponent: () =>
      import('./components/liste/lista-form/lista-form').then((m) => m.ListaForm),
  },
  {
    path: 'lista/:id',
    loadComponent: () =>
      import('./components/liste/lista-detail/lista-detail').then((m) => m.ListaDetail),
  },
  {
    path: '**',
    redirectTo: '',
  },
];
