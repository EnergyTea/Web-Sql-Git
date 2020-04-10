import { Component } from '@angular/core';
@Component({
    selector: 'edit',
    templateUrl: './edit.component.html',
    styleUrls: ['./edit.component.css']

})
export class EditComponent {
  tags = ["Tom", "Bob", "Sam", "Bill", "Tom", "Bob", "Sam", "Bill", "Tom", "Bob", "Sam", "Bill", "Tom", "Bob", "Sam", "Bill", "Tom", "Bob", "Sam", "Bill", "Tom", "Bob", "Sam"];

  async loadScripts() {
  var scripts = [];
  var response = await fetch("/api/scripts");
  if (response.ok) {
    scripts = await response.json();
  } else {
    alert("HTTP ERROR:" + response.status);
  }
   // this.loadScripts(scripts);
}

  async editCode() {
    var id = (<HTMLInputElement>document.getElementById('editIdInput')).value;
    if (!id) return;
    var code = (<HTMLInputElement>document.getElementById('editCodeInput')).value;
    var author = (<HTMLInputElement>document.getElementById('editAuthorInput')).value;
    var fromData = fromData();
    fromData.append("code", code);
    fromData.append("author", author);
    var response = await fetch("/api/scripts/" + id, {
      method: "PUT",
      body: fromData
    });
    if (response.ok) {
      await this.loadScripts();
    } else {
      alert("HTTP ERROR:" + response.status);
    }
    this.loadScripts();
  }

  async addCode() {
    var code = (<HTMLInputElement>document.getElementById('editCodeInput')).value;
    if (!code) return;
    var author = (<HTMLInputElement>document.getElementById('editAuthorInput')).value;
    var fromData = fromData();
    fromData.append("code", code);
    fromData.append("author", author);
    var response = await fetch("/api/scripts/", {
      method: "POST",
      body: fromData
    });
    if (response.ok) {
      await this.loadScripts();
    } else {
      alert("HTTP ERROR:" + response.status);
    }
  }
}
