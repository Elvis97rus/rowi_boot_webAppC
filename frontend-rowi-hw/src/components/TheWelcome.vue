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

  // Fetches data when the component is created.
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
      console.log(this.date);
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
    <div class="details flex justify-center">
      <i><DocumentationIcon /></i>
      <h3>Visits</h3>
    </div>
    <div class="flex">
      <VisitsItem v-for="visit of this.visits" :key="visit.id" :data="visit" />
    </div>

    <WelcomeItem>
      <template #icon>
        <ToolingIcon />
      </template>
      <template #heading>Patients</template>
    </WelcomeItem>

    <WelcomeItem>
      <template #icon>
        <EcosystemIcon />
      </template>
      <template #heading>Medicines</template>
    </WelcomeItem>

    <WelcomeItem>
      <template #icon>
        <CommunityIcon />
      </template>
      <template #heading>Doctors</template>
    </WelcomeItem>
  </div>
</template>
