import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MessagesComponent } from './components/messages/messages.component';
import { ListsComponent } from './components/lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './components/members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './components/members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { ListsResolver } from './_resolvers/lists.resolver';


const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '', // localhost:4200/members
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'members', component: MemberListComponent,
                        resolve: {users: MemberListResolver}},
      {path: 'members/:id', component: MemberDetailComponent,
                            resolve: {user: MemberDetailResolver}},
      /* id for edit is give by the token */
      {path: 'member/edit', component: MemberEditComponent,
                            resolve: {user: MemberEditResolver},
                            canDeactivate: [PreventUnsavedChanges]},
      {path: 'messages', component: MessagesComponent},
      {path: 'lists', component: ListsComponent, resolve: {users: ListsResolver}},
    ]
  },
  {path: '**', redirectTo: '', pathMatch: 'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
