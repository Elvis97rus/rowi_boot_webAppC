<script>
import { ref } from "vue";
import { HTTP } from "../api/http-common";
import VisitsItem from "./VisitsItem.vue";

export default {
  components: { VisitsItem },
  data() {
    return {
      date: "",
      visits: [],
      doctors: [],
      medicines: [],
      patients: [],
      errors: [],
    };
  },
  props: ["msg"],

  created() {
    this.init();
  },
  methods: {
    init() {
      this.getPatients();
      this.getVisits();
      this.getMedicines();
      this.getDoctors();
    },
    dateChanged() {
      return this.date;
    },
    getPatients() {
      HTTP.get(`/api/patients`)
        .then((response) => {
          this.patients = response.data;
          return this.patients;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getMedicines() {
      HTTP.get(`/api/medicines`)
        .then((response) => {
          this.medicines = response.data;
          return this.medicines;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getVisits() {
      HTTP.get(`/api/visits`)
        .then((response) => {
          this.visits = response.data;
          return this.visits;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
    getDoctors() {
      HTTP.get(`/api/doctors`)
        .then((response) => {
          this.doctors = response.data;
          return this.doctors;
        })
        .catch((e) => {
          this.errors.push(e);
        });
    },
  },
};
</script>

<template>
  <div>
    <div class="details justify-center py-5">
      <h1 class="text-xl text-black-800">Hello, here you can:</h1>
      <ul>
        <li>
          create / edit / observe [Doctors]
          <a class="pl-4 text-xl hover:text-green-500" href="/doctors">--></a>
        </li>
        <li>
          create / edit / observe [Patients]
          <a class="pl-4 text-xl hover:text-green-500" href="/patients">--></a>
        </li>
        <li>
          create / edit / observe [Medicines]
          <a class="pl-4 text-xl hover:text-green-500" href="/medicines">--></a>
        </li>
        <li>
          create / edit / observe [Visits]
          <a class="pl-4 text-xl hover:text-green-500" href="/visits">--></a>
        </li>
        <li class="">------------</li>
        <li>
          search [Visits] by date range
          <a class="pl-4 text-xl hover:text-green-500" href="/visits">--></a>
        </li>
        <li>
          Get [Patients] count by diagnosis(disease)
          <a class="pl-4 text-xl hover:text-green-500" href="/visits">--></a>
        </li>
        <li>
          Get [Medicines] sideEffects by [Medicine](medicineId)
          <a class="pl-4 text-xl hover:text-green-500" href="/medicines">--></a>
        </li>
      </ul>
      <hr class="h-5 bg-gray-100 m-auto mt-4" />
      <h2 class="text-lg text-black-700 py-5">
        Firstly make sure [Patient] exists. <br />
        Then create a [Doctor]. [Medicine] is optional.
      </h2>
      <p>For now, there is no nesessary fields.</p>
    </div>
  </div>
</template>
