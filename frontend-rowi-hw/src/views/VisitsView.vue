<script>
import { ref } from "vue";
import { HTTP } from "../api/http-common";
import VisitsItem from "../components/VisitsItem.vue";

export default {
  components: { VisitsItem },
  data() {
    return {
      date: "",
      popupIsActive: 0,
      visits: [],
      doctors: [],
      medicines: [],
      patients: [],
      errors: [],
      newVisit: 0,

      /*  visitDate: "",
      visitPlace: "",
      patient: "",
      doctor: "",
      medicine: "",
      diagnosis: "",
      recommendation: "",*/
    };
  },
  props: ["id"],

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
          console.log(this.visits.filter(this.$route.params.id));
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
    async save() {
      let response = await HTTP.post(`/api/visits`, {
        visitDate: this.visitDate,
        visitPlace: this.visitPlace,
        patient: this.patient,
        doctor: this.doctor,
        medicine: this.medicine,
        diagnosis: this.diagnosis,
        recommendation: this.recommendation,
      });
      if (response.data) {
        this.$toast.success(`Created.`);
        setTimeout(this.$toast.clear, 500);
      }
    },
    getVisitById() {},
    modalClose() {
      this.newVisit = 0;
    },
  },
};
</script>

<template>
  <div>
    <div class="details flex justify-center">
      <i><DocumentationIcon /></i>
      <a href="/visits"><h3>Visits</h3></a>
      <span>
        ---
        <button class="btn btn-sucsess" @click="this.newVisit = 1">
          New
        </button></span
      >
    </div>
    <div class="flex flex-col" v-if="!this.$route.params.id">
      <VisitsItem
        v-for="visit of this.visits"
        :key="visit.id"
        :data="visit"
        :patients="this.patients"
        :doctors="this.doctors"
        :medicines="this.medicines"
      />
    </div>
    <div class="flex flex-col" v-else>
      <span v-for="visit in this.visits" :key="visit.id" class="w-full">
        <VisitsItem
          :data="visit"
          :patients="this.patients"
          :doctors="this.doctors"
          :medicines="this.medicines"
          v-if="visit.id == this.$route.params.id"
        />
      </span>
    </div>
    <!-- This example requires Tailwind CSS v2.0+ -->
    <div
      v-if="this.newVisit"
      class="invisible0 fixed z-10 inset-0 overflow-y-auto"
      aria-labelledby="modal-title"
      role="dialog"
      aria-modal="true"
    >
      <div
        class="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0"
      >
        <div
          class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"
          aria-hidden="true"
        ></div>

        <!-- This element is to trick the browser into centering the modal contents. -->
        <span
          class="hidden sm:inline-block sm:align-middle sm:h-screen"
          aria-hidden="true"
          >&#8203;</span
        >

        <div
          class="relative inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full"
        >
          <div class="mt-10 sm:mt-0">
            <div class="px-12 py-6">
              <VisitsItem
                :data="{}"
                :popup="1"
                :patients="this.patients"
                :doctors="this.doctors"
                :medicines="this.medicines"
                @modalClose="this.modalClose()"
                @patientsRefresh="this.getPatients()"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
