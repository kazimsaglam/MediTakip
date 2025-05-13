"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const prescriptionController_1 = require("../controllers/prescriptionController");
const router = (0, express_1.Router)();
router.get('/list/:code?', prescriptionController_1.prescriptionList);
router.post('/create', prescriptionController_1.createPrescription);
exports.default = router;
