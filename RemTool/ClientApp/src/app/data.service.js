var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Injectable } from '@angular/core';
let DataService = class DataService {
    constructor(http) {
        this.http = http;
        this.urlBrands = "api/brands";
        this.urlTools = "api/tools";
        this.urlSpareParts = "api/spareparts";
    }
    // brands
    getBrands() {
        return this.http.get(this.urlBrands);
    }
    getBrand(id) {
        return this.http.get(this.urlBrands + '/' + id);
    }
    createBrand(brand) {
        return this.http.post(this.urlBrands, brand);
    }
    updateBrand(brand) {
        return this.http.put(this.urlBrands, brand);
    }
    deleteBrand(id) {
        return this.http.delete(this.urlBrands + '/' + id);
    }
    // tools
    getTools() {
        return this.http.get(this.urlTools);
    }
    getTool(id) {
        return this.http.get(this.urlTools + '/' + id);
    }
    createTool(tool) {
        return this.http.post(this.urlTools, tool);
    }
    updateTool(tool) {
        return this.http.put(this.urlTools, tool);
    }
    deleteTool(id) {
        return this.http.delete(this.urlTools + '/' + id);
    }
    // spareParts
    getSpareParts() {
        return this.http.get(this.urlSpareParts);
    }
    getSparePart(id) {
        return this.http.get(this.urlSpareParts + '/' + id);
    }
    createSparePart(sparePart) {
        return this.http.post(this.urlSpareParts, sparePart);
    }
    updateSparePart(sparePart) {
        return this.http.put(this.urlSpareParts, sparePart);
    }
    deleteSparePart(id) {
        return this.http.delete(this.urlSpareParts + '/' + id);
    }
};
DataService = __decorate([
    Injectable()
], DataService);
export { DataService };
//# sourceMappingURL=data.service.js.map